# Full Stack Technical Assessment #

Thank you for your interest in Porch Software! Everyone at Porch brings immense value to the company and has a significant role to play in our success as a whole. As such, we all have a lot of responsibility and take ownership of our work with pride. This assessment will serve to help you understand the basics of our tech stack, while simultaneously giving you the oportunity to dive into a fresh project and show us what you are made of!

### What Are You In For? ###

#### Quick summary ####
You will be standing up a new full stack application geared towards managing Clients and their data. The tech stack you'll be using has been (mostly) laid out and is nicely packaged up for you in a small Docker container. If you are unfamiliar with Docker, that's no problem at all. We'll try to lay things out for you nice and simply down below. Acceptance Criteria (AC) for this exercise is also explained below, and should be very basic. Once you complete the basic ACs, feel free to add your own flair to your code and flex on us a bit. Once you are complete with the task, we'll sync up with you to review your code and discuss your brilliant work.

#### Keeping Track ####
We get that time spent doesn't always clearly indicate skill level or technical prowess, especially since this exercise is somwewhat open-ended. All the same, we expect the basic functionality of this excersice to take you no more than a few hours. Once you get the basics flushed out, feel free to spend as much additional time as you'd like going the extra mile and showing off. However, if you find yourself spending more than a few hours getting the basic functionality of this assessment wired up, please let us know; it's likely that you are either off the intended path, or need to beef up your skillset a bit before reapplying.

### How do I get set up? ###

Getting set up should be quite straight-forward. As this is a Docker project, please make sure you have Docker installed on your machine. Next, you will need to create a GitHub account (if you don't already have one) and fork this repository. Then simply clone your forked repo, checkout a development branch on your machine (e.g. yourname-dev), and boot things up with the commands below.

#### Installing Docker ####
Instructions for installing Docker and getting your feet wet can be found here: https://docs.docker.com/get-started/

#### Running the Project ####
The docker-compose.yml file needed to boot everything up is found in the `client-dashboard/` directory of this repository. To boot, simply navigate to this directory in your terminal and execute the following command `docker-compose -f docker-compose.yml up`

The first time you run this, this will build your Containers and boot everything up.

#### Some Helpful Tips ####
* Feel free to update the version of anything included in this take-home. As long as you use the same general technologies we've laid out here, you're golden.
* After making backend code changes, make sure you run `docker-compose -f docker-compose.yml build` to rebuild and then `docker-compose -f docker-compose.yml up` again in order to see your updates. Hot reloading should be implemented on the frontend, so you won't need to do this when updating your Vue code.
* Debugging the backend is going to be dependent on the IDE that you are using. You'll need to find out how this is accomplished in your development environment on your own, but there are plenty of resources out there that will walk you through it if you just Google, "How to debug .Net Core in Docker".
* Your database and user will be created on your Container's initialization, and won't be recreated after this, no matter how many times the Containner is upped. If you need to blow away your Docker container and do a fresh installation (e.g. when you add or modify SQL scripts for your tables), you can run the follwing commands
	1. `docker-compose -f docker-compose.yml down`
	2. `docker rm -f $(docker ps -a -q)`
	3. `docker volume rm $(docker volume ls -q)` (Note: this will blow away any data in your DB)
	4. `docker-compose -f docker-compose.yml up`
* There's a helpful .NET tool installed in your backend project called Swagger. This will let you view and test your API in the browser without a dedicated frontend. To access this tool, navigate to the following URL once your Containers are running: <http://localhost:5000/swagger/index.html>

### Acceptance Criteria ###

Most of the following ACs will need to be implemented by you. These ACs serve more as a guide for what we'd like to see you accomplish rather than hard and fast rules. Don't stress too much over the minutia here. We really just want a good sampling of what you are capable of. As long as you implement each of the following bullet points in some fashion, we'll be happy to pick it apart with you.

#### Database ####
* We need a table to store Client records
* We need a table to store Phone numbers
* We need to associate a client with any number of phone numbers

Reminder: Make sure you execute the commands found in the Helpful Tips section above to blow away your database after making schema changes. If you don't, your existing database will take precedence over your new changes and you might not see your updates like you expect.

#### API ####
The following endpoints are created

* GetClient - Returns a single client record, given some identifier
* GetAllClients - Returns all client records from the database
* CreateClient - Creates a new client record and stores it in database
* UpdateClient - Updates information on a client record
* ArchiveClient - Archives a client record without deleting it

#### UI ####
The following pages are created

* Client Dashboard
	* Create New button is present. Clicking this button will display a form that will allow the user to create a new client.
	* Displays all unarchived clients
	* Each existing client can be clicked on, which will route the user to a page in which client data can be updated
* Client Management Page
	* Client name and phone number information is editable on this page (make sure multiple phone numbers can be added/edited/removed)
	* A save button will persist the updates and return you back to the dashboard
	* There is also an archive button in this page that will archive the Client record and return you to the dashboard

### When You're Finished ###

You should have been doing your work on a local development branch, as instructed above in the "How do I get set up?" section. Once you have completed the exercise, if you have not done so already, please publish your branch. Then submit a Pull Request from your branch (in your forked repo) to a **new branch** in our repo (the repo you forked from) and send us an email, letting us know you are all set. We'll reach out and review your work with you. We're looking forward to it!

### Need to Reach Out? ###
Feel free to reach out to us if you need anything!

* Bruce Cragun / Engineering Manager - bcragun@porch.software
* Nick Farland / Engineering Manager - nfarland@porch.software
* Shane Wilks / Engineering Manager - swilks@porch.software