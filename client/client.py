import grpc
import ping_pb2
import ping_pb2_grpc

def main():
    
    # ... create an insecure gRPC channel
    channel = grpc.insecure_channel("localhost:5000")
    
    # ... create the stub, which abstracts away:
    # ... - HTTP/2
    # ... gRPC: Google Remote Procedure Call
    # ... Protobuf: Protocol Buffer
    stub = ping_pb2_grpc.PingStub(channel)

    # ... send a request to the server
    request = ping_pb2.PingRequest(message="Hello from Python")
    
    # ... list out for the response
    response = stub.SayPing(request)

    # ... print the response
    print(f"✅ Response from C#: {response.message}")


if __name__ == "__main__":
    
    main()