#!docker build -t vaishnavitv/rest-server .
#docker run -p 50770:50770 --rm -it vaishnavitv/rest-server

FROM microsoft/aspnetcore-build

# Copy csproj and restore as distinct layers
COPY ./REST-Server/ /REST-Server
RUN mv /REST-Server/WWWRoot /REST-Server/wwwroot

WORKDIR /REST-Server
RUN dotnet restore
RUN dotnet build

EXPOSE 50770
ENTRYPOINT ["dotnet", "run", "--urls", "http://*:50770"]
