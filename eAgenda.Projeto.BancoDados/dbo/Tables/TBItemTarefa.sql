CREATE TABLE [dbo].[TBItemTarefa] (
    [Numero]        INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]        VARCHAR (300) NOT NULL,
    [Concluido]     BIT           NOT NULL,
    [Tarefa_Numero] INT           NOT NULL,
    CONSTRAINT [PK_TBItemTarefa] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBItemTarefa_TBTarefa] FOREIGN KEY ([Tarefa_Numero]) REFERENCES [dbo].[TBTarefa] ([Numero])
);

