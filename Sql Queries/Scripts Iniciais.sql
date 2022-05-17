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
	)	

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
