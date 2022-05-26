CREATE TABLE [dbo].[TBDespesa_TBCategoria]
(
	[Despesa_Numero] INT NOT NULL , 
    [Categoria_Numero] INT NOT NULL, 
	CONSTRAINT [FK_TBCategoria_TBDespesa] FOREIGN KEY([Categoria_Numero]) REFERENCES [dbo].[TBCategoria] ([Numero]),
	CONSTRAINT [FK_TBDespesa_TBCategoria] FOREIGN KEY([Despesa_Numero]) REFERENCES [dbo].[TBDespesa] ([Numero])
)
