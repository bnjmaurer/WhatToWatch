#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM whattowatchapi:latest AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WhatToWatch.Tests/WhatToWatch.Tests.csproj", "WhatToWatch.Tests/"]
COPY ["WhatToWatch.Api/WhatToWatch.Api.csproj", "WhatToWatch.Api/"]
RUN dotnet restore "WhatToWatch.Tests/WhatToWatch.Tests.csproj"
COPY . .
WORKDIR "/src/WhatToWatch.Tests"
RUN dotnet build "WhatToWatch.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatToWatch.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "/app/publish/WhatToWatch.Tests.dll"]