import grpc
import arrow_pb2
import arrow_pb2_grpc
import pyarrow as pa
from io import BytesIO

def create_arrow_payload():
    
    # Define two small arrays
    array1 = pa.array([1.0, 2.0, 3.0])
    array2 = pa.array([4.0, 5.0, 6.0])
    
    # Name the arrays and batch them together
    batch = pa.RecordBatch.from_arrays([array1, array2], names=["x", "y"])

    # Send the arrays as a ByteStream rather than as one block
    # ... good for scalability
    sink = BytesIO()
    
    with pa.ipc.RecordBatchStreamWriter(sink, batch.schema) as writer:
        writer.write_batch(batch)

    return sink.getvalue()

def main():
    
    # Create an (insecure) channel
    channel = grpc.insecure_channel('localhost:5000')
    
    # The stub abstracts away the gRPC / Protobuf mechanics
    stub = arrow_pb2_grpc.ArrowServiceStub(channel)

    #  Create the data payload (as a ByteStream)
    payload = create_arrow_payload()
    
    # Send a request to the server
    request = arrow_pb2.ArrowRequest(data=payload)

    # Exception handling for anything passing over a network
    try:    
        
        # ... receive the reply object
        reply = stub.SayArrow(request)
        
        # ... two toy functions have been executed
        # ... 1. return the names of the arrays
        # ... 2. return the sums of values of arrays
        print("Array names:", reply.array_names)
        print("Array sums:", reply.array_sums)
        
    except grpc.RpcError as e:
        
        print("RPC failed:", e.code(), e.details())

if __name__ == "__main__":
    
    main()