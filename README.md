# model-service-arrow

This repo extends the `model-service` repo to include the use of `Apache Arrow` for zero-copy data transfer. It uses clear, minimal code to demonstrate the skeleton of a modern MaaS API architecture:

1) Remote Procedure Calls using `gRPC`.
2) Client-server contract specified using `Protobuf`.
3) Zero-copy data transfer using `Apache Arrow`.

### What are the architectural requirements of a MaaS API?

1) REST and GraphQL APIs are - in a sense - data first. MaaS requires a function-first API which invokes remote procedure calls. A modern implementation of RPCs is Google's gRPC. So let's use that as a building block.

2) A Models-as-a-Service API requires clarity between client and server as to which functions are available. We use Protocol Buffer (Protobuf) to define the contract between them.

3) Many APIs require data serialisation, deserialisation and copying. For reasons of speed and accuracy, it would be better if we could use a zero-copy mechanism, such as Apache Arrow. So let's use that.

4) Putting together a) a function-first API using gRPC, b) a client-server contract in the form of Protobuf and c) zero-copy data transfer in the form of Apache Arrow, we arrive - almost inevitably - at Arrow Flight.

That's a good architecture for a MaaS API.

### What does this repo demonstrate?

The code allows a Python client to call parameterised functions from a remote server. For a function $f(x)$, the function $f$ is executed on the server. The data $x$ is passed as `Arrow`, which allows the passing of multiple named arrays using zero-copy data transfer.

1) `arrow.proto`: defines the Protobuf client-server contract that specifies the functions available to the client.  
2) `arrow_pb2.py` and `arrow_pb2_grpc.py`: generated automatically from `arrow.proto` by gRPC as client-side contract definitions.
3) `client_arrow.py`: creates an (insecure) channel and a stub that abstracts the Protobuf and gRPC mechanics underneath. The stub is used to call parameterised functions executed on a remote server.
4) `server_csharp`: server side code. gRPC is capable of generating a lot of this code automatically. I found it difficult to make the auto-generated code run cleanly and reliably so I re-wrote it manually. 

This repo demonstrates a function-first API with a contract between client and server using zero-copy data transfer. The code is deliberately minimal and clear for purposes of exposition.

### What else?

1) The C# server solution contains a `Dockerfile` allowing the code to be run in a Docker container. This has been tested and runs successfully.  
2) The purpose of the repo is to demonstrate use of `gRPC`, `Protobuf` and `Arrow` in the construction of a model service API. The repo is a demonstration-of-concept. It doesn't include any of the machinery needed for a commercial API, such as logging, authentication or a health service. Neither does it include code for a graceful shutdown, allowing `gRPC` to release resources.
3) This repo is based on - and extends - my `model-service` repo.
