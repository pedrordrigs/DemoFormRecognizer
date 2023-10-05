# -*- coding: utf-8 -*-
import pandas as pd
import sys
import json
import xml.etree.ElementTree as ET
import re
import time

def generate_excel_and_xml_from_custom_json(custom_json):
    # Preparar uma lista para armazenar os dados de cada linha como um dicionário para o Excel
    
    excel_data = []
    
    # Criar uma estrutura de árvore XML para o XML
    
    root = ET.Element("root")
    
    # Processar o campo 'Tables' no JSON
    
    for table in custom_json.get('Tables', []):
        for cell in table.get('Cells', []):
            column_index = cell.get('ColumnIndex', 0)
            row_index = cell.get('RowIndex', 0)
            text = cell.get('Text', '')
            
            # Ampliar a lista de dicionários para cobrir todas as linhas
            
            while len(excel_data) <= row_index:
                excel_data.append({})
            
            excel_data[row_index][column_index] = text
            
            # Adicionar ao XML
            
            row_element = ET.SubElement(root, f"Linha{row_index}")
            cell_element = ET.SubElement(row_element, f"Celula{column_index}")
            cell_element.text = text
    
    # Gerar arquivo Excel
    
    df = pd.DataFrame(excel_data).sort_index(axis=1)
    timestamp = str(int(time.time()))
    excel_path = f'Outputs/dados_customizados_corrigidos_{timestamp}.xlsx'
    xml_path = f'Outputs/dados_customizados_corrigidos_{timestamp}.xml'
    
    df.to_excel(excel_path, index=False)
    ET.ElementTree(root).write(xml_path)
    
    return excel_path, xml_path

if __name__ == "__main__":
    temp_file_path = sys.argv[1]  
    with open(temp_file_path, 'r', encoding='utf-8') as f:
        json_str = f.read()
    try:
        json_data = json.loads(json_str)
        if isinstance(json_data, list):  # Verificar se é uma lista
            for item in json_data:
                generate_excel_and_xml_from_custom_json(item)
        else:  # Assumir que é um dicionário
            generate_excel_and_xml_from_custom_json(json_data)
        print("Excel e XML gerados com sucesso.")
    except Exception as e:
        print(f"Erro ao gerar Excel e XML: {e}")
