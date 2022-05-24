CREATE TABLE [dbo].[TBContato] (
    [Numero]   INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (300) NOT NULL,
    [Email]    VARCHAR (300) NULL,
    [Telefone] VARCHAR (20)  NULL,
    [Empresa]  VARCHAR (300) NULL,
    [Cargo]    VARCHAR (300) NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC)
);

