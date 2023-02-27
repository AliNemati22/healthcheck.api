FROM cr.internals.emofid.com/easytrader/fat-image/dotnet-6/prod:latest
WORKDIR /app
COPY /app/publish .
ARG CI_PIPELINE_ID
ENV CI_PIPELINE_ID=$CI_PIPELINE_ID
ENTRYPOINT ["dotnet", "healthcheck.api.dll"]
EXPOSE 80 443