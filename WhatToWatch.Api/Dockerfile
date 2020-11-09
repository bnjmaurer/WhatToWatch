#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# hicolor-icon-theme \
# libcanberra-gtk* \
# libgl1-mesa-dri \
# libgl1-mesa-glx \
# libpango1.0-0 \
# libpulse0 \
# libv4l-0 \
# fonts-symbola \

# Install Chrome
RUN apt-get update && apt-get install -y \
apt-transport-https \
ca-certificates \
curl \
gnupg \
--no-install-recommends \
&& curl -sSL https://dl.google.com/linux/linux_signing_key.pub | apt-key add - \
&& echo "deb [arch=amd64] https://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google.list \
&& apt-get update && apt-get install -y \
google-chrome-stable \
--no-install-recommends \
&& apt-get purge --auto-remove -y curl \
&& rm -rf /var/lib/apt/lists/*

# Download the google-talkplugin And ChromeDrive
RUN set -x \
&& apt-get update \
&& apt-get install -y --no-install-recommends \
ca-certificates \
curl \
unzip \
&& rm -rf /var/lib/apt/lists/* \
&& mkdir \opt\selenium \
&& curl -sSL "https://chromedriver.storage.googleapis.com/2.40/chromedriver_linux64.zip" -o /tmp/chromedriver.zip \
&& unzip -o /tmp/chromedriver -d /opt/selenium/ \
&& rm -rf /tmp/*.deb \
&& apt-get purge -y --auto-remove curl unzip

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WhatToWatch.Api/WhatToWatch.Api.csproj", "WhatToWatch.Api/"]
RUN dotnet restore "WhatToWatch.Api/WhatToWatch.Api.csproj"
COPY . .
WORKDIR "/src/WhatToWatch.Api"
RUN dotnet build "WhatToWatch.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhatToWatch.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WhatToWatch.Api.dll"]

