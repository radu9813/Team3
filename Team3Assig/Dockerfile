#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
<<<<<<< HEAD
EXPOSE 80
EXPOSE 443
=======
>>>>>>> 605adfc25fd790cd6bf76e149d600c8d013a7e84

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Team3Assig.csproj", "."]
RUN dotnet restore "./Team3Assig.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Team3Assig.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Team3Assig.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Team3Assig.dll"]