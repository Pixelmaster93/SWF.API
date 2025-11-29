# Usa l'immagine ufficiale di Microsoft per compilare
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia il file di progetto e ripristina le dipendenze
COPY ["ShitWithFriendAPI/ShitWithFriendAPI.csproj", "ShitWithFriendAPI/"]
RUN dotnet restore "ShitWithFriendAPI/ShitWithFriendAPI.csproj"

# Copia tutto il resto del codice e compila
COPY . .
WORKDIR "/src/ShitWithFriendAPI"
RUN dotnet build "ShitWithFriendAPI.csproj" -c Release -o /app/build

# Pubblica l'app
FROM build AS publish
RUN dotnet publish "ShitWithFriendAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Immagine finale per l'esecuzione (leggera)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

# Crea una cartella per il database
RUN mkdir -p /app/data

# Imposta il punto di ingresso
ENTRYPOINT ["dotnet", "ShitWithFriendAPI.dll"]
