CREATE TABLE [dbo].[TBCompromisso] (
    [Numero]         INT            IDENTITY (1, 1) NOT NULL,
    [Assunto]        VARCHAR (300)  NULL,
    [Local]          VARCHAR (300)  NULL,
    [Link]           VARCHAR (1000) NULL,
    [Data]           DATETIME       NOT NULL,
    [HoraInicio]     BIGINT         NOT NULL,
    [HoraTermino]    BIGINT         NOT NULL,
    [Contato_Numero] INT            NULL,
    CONSTRAINT [PK_TBCompromisso] PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_TBCompromisso_TBContato] FOREIGN KEY ([Contato_Numero]) REFERENCES [dbo].[TBContato] ([Numero])
);

