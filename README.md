# HttpRequest Processor Background Process
This Project is for creating a service to handle massive amounts of  job processing in parallel in background


## *Introduction*


The purpose of this API is to run massively parallel computing tasks in the background without any compromise to the performance and with scalability.

We are employing components such as Background tasks with hosted service, Concurrent Queues and cloud components such as API Gateway , ECS Clusters and Databases.

In the below block diagram, I am explaining the flow of the traffic and how the individual components interact with each other.


## *Architecture*

![Block Diagram CM](https://user-images.githubusercontent.com/1848726/208124135-5ae42e29-6f91-4267-b372-0ac349e1b0d4.png)



1. Route 53 does the domain resolution and redirects traffic to the API Gateway

2. Incoming traffic from  the routes  are routed to the ECS/EKS Cluster

3. Each ECS Cluster hosts the Http Request Processor service and auto scales it on demand based on cpu load/ memory load and other constraints

4. With each service , there are calls to Concurrent Queues those are thread safe and Background workers that work on heavy processing tasks and do a callback to the webhook url supplied in the incoming request .

5. The Job Statuses are saved in the DB for all the stages in the process from NOT_STARTED, STARTED, FINISHED_SUCCESS, FINISHED_FAILED or FINISHED_TIMEOUT

6. The endpoint check-job-status checks for the status of the current job. This is called periodically for checking on job statuses.

7. The Number of simultaneous jobs that can be run at a time are by default to 8.

8. The above design is also scalable with increased requests.

## *How to Run this Application*

Download the code and run it on https://localhost:5001/swagger

It will show the 2 endpoints. 


## *Improvements*

Right now for the time consuming job its just having commented instructions. After doing an actual live implementation, more improvements needs to be done to tweak the solution.

I have got the gist of the problem statement and have all the necessary logic and hooks from where we can make more improvements.

I really enjoyed doing this project and learned a lot in the process. Thank You for the Opportunity.
