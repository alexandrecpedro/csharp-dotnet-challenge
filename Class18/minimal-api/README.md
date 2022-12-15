# Como criar as databases de test e dev
```shell
cd api
DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7_test;Uid=root;Pwd=root" dotnet ef database update # create base de test

DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7;Uid=root;Pwd=root" dotnet ef database update # create base de dev ou prod

##### ou #####
./migration.sh
```

# Rodando test
```shell
DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7_test;Uid=root;Pwd=root" dotnet test # roda test

##### ou #####
./test.sh
```

# Rodando Aplicação
```shell
cd api
DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7;Uid=root;Pwd=root" dotnet run # roda app

##### ou #####
./run.sh
```

# Rodando para ambiente de produção
```shell
./run-prod.sh
```