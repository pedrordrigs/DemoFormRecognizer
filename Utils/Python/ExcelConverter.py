# -*- coding: utf-8 -*-
import pandas as pd
import sys
import json
import xml.etree.ElementTree as ET
import re
import time

def generate_excel_and_xml_from_custom_json(custom_json):
    # Prepare a list to store each row data as a dictionary for Excel
    excel_data = []
    
    # Create an XML tree structure for XML
    root = ET.Element("root")
    
    # Process the 'Tables' field in the JSON
    for table in custom_json.get('Tables', []):
        for cell in table.get('Cells', []):
            column_index = cell.get('ColumnIndex', 0)
            row_index = cell.get('RowIndex', 0)
            text = cell.get('Text', '')
            
            # Extend the list of dictionaries to cover all rows
            while len(excel_data) <= row_index:
                excel_data.append({})
            
            excel_data[row_index][column_index] = text
            
            # Add to XML
            row_element = ET.SubElement(root, f"Row{row_index}")
            cell_element = ET.SubElement(row_element, f"Cell{column_index}")
            cell_element.text = text
    
    # Generate Excel file
    df = pd.DataFrame(excel_data).sort_index(axis=1)
    timestamp = str(int(time.time()))
    excel_path = f'Outputs/custom_data_fixed_{timestamp}.xlsx'
    xml_path = f'Outputs/custom_data_fixed_{timestamp}.xml'
    
    df.to_excel(excel_path, index=False)
    ET.ElementTree(root).write(xml_path)
    
    return excel_path, xml_path

if __name__ == "__main__":
    temp_file_path = sys.argv[1]  
    with open(temp_file_path, 'r', encoding='utf-8') as f:
        json_str = f.read()
    try:
        json_data = json.loads(json_str)
        if isinstance(json_data, list):  # Verifique se é uma lista
            for item in json_data:
                generate_excel_and_xml_from_custom_json(item)
        else:  # Assuma que é um dicionário
            generate_excel_and_xml_from_custom_json(json_data)
        print("Excel e XML gerados com sucesso.")
    except Exception as e:
        print(f"Erro ao gerar Excel e XML: {e}")

