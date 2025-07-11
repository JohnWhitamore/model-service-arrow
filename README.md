# model-service

Let's think about Models as a Service (MaaS).

### Architectural premise

1) REST and GraphQL APIs are - in a sense - data first. MaaS requires a function-first API which invokes remote procedure calls. A modern implementation of RPCs is Google's gRPC. So let's use that as a building block.

2) A Models-as-a-Service API requires clarity of which functions are available between client and server. We use Protocol Buffer (or Protobuf) to define the contract between them.

3) Many APIs require serialisation of data, deserialising the data and copying the data. It would be better if we could use a zero-copy mechanism, such as Apache Arrow. So let's use that.

4) Putting together a) a function-first API using gRPC, b) a client-server contract in the form of Protobuf and c) zero-copy data transfer in the form of Apache Arrow, we arrive - almost inevitably - at Arrow Flight.

That's a pretty good architecture for a MaaS API.

### What this repo demonstrates

The code allows a Python client to send a message to a C# server and receive back a response.

1) `proto:ping.proto`: defines a Protobuf client-server contract.  
2) `ping_pb2.py` and `ping_pb2_grpc.py`: generated automatically from `ping.proto` by gRPC as client-side contract definitions.
3) `client.py`: creates an (insecure) channel and a stub that abstracts the Protobuf and gRPC mechanics underneath. Uses the stub to ping a message to the server and receive a response.
4) `server_csharp`: server side code. gRPC is capable of generating a lot of this code automatically. I found it difficult to make the auto-generated code run cleanly and reliably so I re-wrote it manually. 

This repo is a first principled step towards the infrastructure of function-first, zero-copy model serving. It does not yet use Arrow Flight. It does use gRPC and Protobuf.
