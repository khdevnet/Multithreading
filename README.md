# Thread Synchronization Constructs

### There are two kinds of primitive constructs:
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
   
**livelock** - if thread blocking using a user-mode construct and the thread is running on a CPU forever;    
**deadlock** - if thread blocking using a kernel-mode construct, the thread is blocked forever.

A **deadlock** is always preferable to a livelock, because a livelock wastes both CPU time and memory (the thread’s stack, etc.), whereas a
deadlock wastes only memory.

There are two kinds of primitive user-mode thread synchronization constructs:
- **Volatile** constructs, which perform an atomic read or write operation on a variable containing a simple data type at a specific time
- **Interlocked** constructs, which perform an atomic read and write operation on a variable containing a simple data type at a specific time

#### Monitor/lock
Monitor.Enter is not a normal .NET method (can't be decompiled with ILSpy or similar). The method is implemented internally by the CLR, so strictly speaking, there is no one answer for .NET as different runtimes can have different implementations.     
All objects in .NET have an object header containing a pointer to the type of the object, but also an SyncBlock index into a SyncTableEntry. Normally that index is zero/non used, but when you lock on the object it will contain an index into the SyncTableEntry which then contains the reference to the actual lock object.

#### Mutexes
**Mutex** is a synchronization primitive that grants exclusive access to the shared resource to only one thread. If a thread acquires a mutex, the second thread that wants to acquire that mutex is suspended until the first thread releases the mutex.      
Mutexes are of two types: 
* **local mutex** exists only within your process. It can be used by any thread in your process that has a reference to the Mutex object that represents the mutex
* **Named system** mutexes are visible throughout the operating system, and can be used to synchronize the activities of processes.

#### Semaphores
**Semaphore** limit the number of threads that can access a shared resource or a pool of resources concurrently. Additional threads that request the resource wait until any thread releases the semaphore. Because the semaphore doesn't have thread affinity, a thread can acquire the semaphore and another one can release it.
