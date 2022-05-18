--Criando a tabela de Contatos
	CREATE TABLE [dbo].[TBContato] (
		[Numero]   INT           IDENTITY (1, 1) NOT NULL,
		[Nome]     VARCHAR (300) NOT NULL,
		[Email]    VARCHAR (300) NULL,
		[Telefone] VARCHAR (20)  NULL,
		[Empresa]  VARCHAR (300) NULL,
		[Cargo]    VARCHAR (300) NULL,
		PRIMARY KEY CLUSTERED ([Numero] ASC)
	);


--Inserindo um registro na tabela
	INSERT INTO [TBCONTATO] 
	(
		[NOME], 
		[EMAIL],
		[TELEFONE],
		[EMPRESA],
		[CARGO]
	)
	VALUES 
	(
		'GABRIEL BARBOSA',
		'GABIGOL@GMAIL.COM',
		'49 9 99999999',
		'FLAMENGO',
		'ARTILHEIRO'
	); 
	select Scope_Identity();

--Atualizando um registro da tabela
	UPDATE [TBCONTATO]	
		SET
			[NOME] = 'RODRIGO CAIO',
			[EMAIL] = 'XERIFE@MENGAO.COM',
			[TELEFONE] = '987654321',
			[EMPRESA] = 'FLAMENGO',
			[CARGO] = 'ZAGUEIRO'
		WHERE
			[NUMERO] = 2

--Excluindo um registro da tabela
	DELETE FROM [TBCONTATO]
		WHERE 
			[NUMERO] = 4

--Retornando todos os registros da tabela
	SELECT 
		[NUMERO], 
		[NOME], 
		[EMAIL],
		[TELEFONE],
		[EMPRESA],
		[CARGO]
	FROM 
		[TBCONTATO]

--Retornando apenas um registro da tabela 
	SELECT 
		[NUMERO], 
		[NOME], 
		[EMAIL],
		[TELEFONE],
		[EMPRESA],
		[CARGO]
	FROM 
		[TBCONTATO]
	WHERE 
		[NUMERO] = 1

select count(*) from TBContato