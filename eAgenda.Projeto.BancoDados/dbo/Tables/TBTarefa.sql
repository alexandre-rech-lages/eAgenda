CREATE TABLE [dbo].[TBTarefa] (
    [Numero]              INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]              VARCHAR (500) NOT NULL,
    [Prioridade]          INT           NOT NULL,
    [DataCriacao]         DATETIME      NOT NULL,
    [DataConclusao]       DATETIME      NULL,
    [PercentualConcluido] DECIMAL (18)  NULL,
    CONSTRAINT [PK_TBTarefa] PRIMARY KEY CLUSTERED ([Numero] ASC)
);

