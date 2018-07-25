#First Run: docker build -t vaishnavitv/rest-server .
#Next Run: docker run -p 50770:50770 --rm -it vaishnavitv/rest-server

FROM microsoft/aspnetcore-build as builder

# Copy csproj and restore as distinct layers
WORKDIR /tmp
RUN git clone https://github.com/vaishnavitv/REST-Server-Example.git && \
    cd REST-Server-Example/REST-Server && \
    dotnet restore && \
    dotnet build && \
    dotnet publish -o /REST-Server-Output && \
    mv WWWRoot /REST-Server-Output/wwwroot && \
    rm -fr /tmp/REST-Server-Example

FROM microsoft/aspnetcore

WORKDIR /REST-Server-Output
COPY --from=builder /REST-Server-Output .

EXPOSE 50770
ENTRYPOINT ["dotnet", "REST-Server.dll", "--urls", "http://*:50770"]
