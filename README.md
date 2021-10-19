***Please refer to Considerations below.***

# beerquest-engineering-challenge

## The Challenge
The core data for this challenge is based on pubs around Leeds. The raw data can be found
here and is based around the Leeds Beer Quest
X-Lab would like to build an application using this data, that presents information about pubs
in and around Leeds to their staff so that they can choose an appropriate post-work watering
hole based on their location, rating and services offered.
This challenge can be attempted in one of 3 ways, depending on core discipline.
- **Front end focussed:** Use the API provided at https://datamillnorth.org/dataset/leeds-beer-quest
(this wraps the above dataset) to build a front end application to present this
dataset in a searchable manner to end users
- **Back end focussed:** Using the above dataset, build an API that can be used by front end
applications.
- **Infrastructure focussed:** Deliver a working infrastructure to house all components of the system. We
will provide base containers on request, or you may use your own.

## Considerations/Assumptions
- The approach chosen was **Backend Focused**.
- As can be observed via the commit history, the design is DDD and implementation was done in TDD.
- I have focused on querying a specific Pub to showcase how to do it and how everything evolves into the Domain and Data layers.
- The `HttpClientService` is tested indirectly as it is *difficult* to mock the HttpClient class. It can be done but requires a lot of workarounds, which could be worth it on a real scenario where it will be used a lot. For the sake of this test, it was skipped and tested from outside.
- The models and consequently the domain are very basic at this point but they are ready to be expanded. I would be happy to load all the data in the models down the road, but not everything will make sense being part of the domain. Some parts of the api response are geared towards the data structure, so I believe it would be the Data Layer responsibility to deal with them.
- I decided to query by name and am aware that some names contain characters that will need to be parsed, such as the pub `Wetherspoon's, Railway Station`. Now that we have the happy path working it would be a matter of identifying these edge cases, add tests for them and implement.
- Last but not least, I have spent a few hours in this. There was some setup to make but the architecture evolved as the code was implemented, but naturally I had an idea of the outcome beforehand. I believe that, despite there is only one feature implemented (Get by Name), it is enough to showcase my approach to the problem. More details can be discussed further on, like next steps, reasoning behind some decisions, etc.  
