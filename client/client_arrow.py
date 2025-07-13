import grpc
import arrow_pb2
import arrow_pb2_grpc
import pyarrow as pa
from io import BytesIO

def create_arrow_payload():
    
    array1 = pa.array([1.0, 2.0, 3.0])
    array2 = pa.array([4.0, 5.0, 6.0])
    
    batch = pa.RecordBatch.from_arrays([array1, array2], names=["x", "y"])

    sink = BytesIO()
    
    with pa.ipc.RecordBatchStreamWriter(sink, batch.schema) as writer:
        writer.write_batch(batch)

    return sink.getvalue()

def main():
    
    channel = grpc.insecure_channel('localhost:5000')
    stub = arrow_pb2_grpc.ArrowServiceStub(channel)

    payload = create_arrow_payload()
    request = arrow_pb2.ArrowRequest(data=payload)

    try:    
        reply = stub.SayArrow(request)
        print("Array names:", reply.array_names)
        print("Array sums:", reply.array_sums)
    except grpc.RpcError as e:
        print("RPC failed:", e.code(), e.details())

if __name__ == "__main__":
    
    main()