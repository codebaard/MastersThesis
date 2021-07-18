import pandas as pd

def open(filepath):
    df = pd.read_csv(filepath, sep=';')

    return df