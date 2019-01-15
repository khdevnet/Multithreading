Primitive Thread Synchronization Constructs

There are two kinds of primitive constructs:
1. user-mode      
   Pros:
   - they are significantly faster because they use special CPU instructions to coordinate threads.
   - block the thread for an incredibly short period of time.     
   Cons:
   - Windows operating system never detects that a thread is blocked the thread pool will not create a new thread to replace the temporarily blocked thread
2. kernel-mode    
   Pros:
   - when resource is using by another thread it can block the thread which want to get the access, so that it is no longer wasting CPU time.      
   Cons:
   - they are slower because they use the operating system kernel(Having threads transition from user mode to kernel mode and back incurs a big performance hit).
   
livelock - if thread blocking using a user-mode construct and the thread is running on a CPU forever;
deadlock - if thread blocking using a kernel-mode construct, the thread is blocked forever.

A deadlock is always preferable to a livelock, because a livelock wastes both CPU time and memory (the thread’s stack, etc.), whereas a
deadlock wastes only memory.

There are two kinds of primitive user-mode thread synchronization constructs:
- Volatile constructs, which perform an atomic read or write operation on a variable containing a simple data type at a specific time
- Interlocked constructs, which perform an atomic read and write operation on a variable containing a simple data type at a specific time
