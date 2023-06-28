create table planoconta(
	id int identity(1,1) not null, 
	descricao varchar(50) not null, 
	tipo char(1) not null, 
	primary key(id)
	)
	go


	select * from planoconta


	insert into planoconta(descricao, tipo) values('Combustível', 'D')
	insert into planoconta(descricao, tipo) values('Salário', 'R')
	insert into planoconta(descricao, tipo) values('Alimentação', 'D')

	insert into planoconta(descricao, tipo) values('Impostos', 'D')
	insert into planoconta(descricao, tipo) values('Água', 'D')
	insert into planoconta(descricao, tipo) values('Luz', 'D')
	insert into planoconta(descricao, tipo) values('Internet', 'D')
	insert into planoconta(descricao, tipo) values('Cartão de Crédito', 'D')
	insert into planoconta(descricao, tipo) values('Gastos com a Lata Velha', 'D')

	create table transacao(
		id int identity(1,1) not null,
		data datetime not null, 
		valor decimal(9,2) not null, 
		historico varchar(100) null,
		planoconta_id int not null,
		primary key(id),
		foreign key(planoconta_id) references planoconta(id)
		)
		go

		insert into transacao(data, valor, historico, planoconta_id)
		values (GETDATE(), 25, 'Café de Amanhã', 4)

		insert into transacao(data, valor, historico, planoconta_id)
		values (GETDATE(), 38, 'Gasolina Moto', 1)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230608 15:30', 435, 'Gasolina Carro', 1)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230613', 1000, 'Salário Empresa 1', 2)
				
		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230610', 350, 'IPTU', 5)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230608', 250, 'Copasa', 6)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230608', 387, 'CEMIG', 7)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230605', 450, 'Cilindro de Embreagem do Carro', 10)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230605', 125, 'Internet Casa', 8)

		insert into transacao(data, valor, historico, planoconta_id)
		values ('20230612', 6000, 'Cartão Santander', 9)

		--Quantas transações já foram realizadas
		select count(*) as qtde from transacao

		--Transações do dia 08 de junho de 2023
		select * from transacao 
		where data >= '20230608 00:00:00.000' and
		data <='20230608 23:59:59.000'


		--Total de Receitas e Despesas
		select sum(valor) as total
		from transacao
		where tipo = 'R'

		select sum(valor) as total
		from transacao
		where tipo = 'D'

		select * from transacao