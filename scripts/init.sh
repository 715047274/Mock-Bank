#!/bin/zsh
# initial script for the database migration 


### EF Migration
dotnet ef migrations add initialCreate --startup-project ../../presentation/MockBank.WebApi
dotnet ef database update --startup-project ../../presentation/MockBank.WebApi
dotnet ef migrations remove --startup-project ../../presentation/MockBank.WebApi


#$ mkdir conf.d
#$ dotnet dev-certs https --clean
#$ dotnet dev-certs https -ep ./conf.d/https/dev_cert.pfx -p madison
#$ dotnet dev-certs https --trust

#docker