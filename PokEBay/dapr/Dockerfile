FROM daprio/daprd:latest AS base
ADD nscacert.pem /etc/ssl/certs/nscacert.pem
ADD nstenantcert.pem /etc/ssl/certs/nstenantcert.pem
#RUN update-ca-certificates
 
FROM base AS final