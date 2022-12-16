cd api
echo "====[Rodando migração test]===="
DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7_test;Uid=root;Pwd=root" dotnet ef database update # create base de test
echo "====[Rodando migração dev]===="
DATABASE_URL="Server=localhost;Database=desafio21dias_dotnet7;Uid=root;Pwd=root" dotnet ef database update # create base de dev ou prod