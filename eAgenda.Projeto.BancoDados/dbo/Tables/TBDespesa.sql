CREATE TABLE [dbo].[TBDespesa]
(
	[Numero] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Descricao] VARCHAR(300) NOT NULL, 
    [Valor] NUMERIC NOT NULL, 
    [Data] DATETIME NOT NULL, 
    [FormaPagamento] INT NOT NULL, 
    [Categoria_Numero] INT NOT NULL
)
