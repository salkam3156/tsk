### Decision 1: Adding an external package dependency to Domain

*Context:* Due to time constraints, ValueOf library has been made a dependency of the application Core. This functionality should be removed by self-rolling an implementaion of a ValueObject pattern, if deemed a risk.
Decision has been made, as the value provided at early stages of development is significant in terms of expediting the process, and the switchover, if needed, is trivial.

### Decision 2: Making truck descritpion a part of the Truck entity, while not limiting the description size

*Context:* A truck may have a description.
For ease of modelling it was decided to make it a part of the Truck model, as opposed to having an indirect association via identifier. This should be changed for efficiency later, for efficiency, as truck description 
is mainly a presentation / readmodel-bound-for-UI concern, and should not have much bearing on the processing logic concerning change of state etc.

### Decision 3: Not extending the Truck availabilty with an Unassinged status

*Context:* The requirements specify the need fo CRUD operations in the API. No assumptions are being made about the integrity of the data in the commands of Truck creation.
This may lead to scenarios, where a Truck is created in a way that need manuall / on-demand / on-schedule correction via integration with another module etc.
Theoretically a Truck could be created in undetermined state and left up for grabs and processing to all the established codepaths, but this requirement would do best with clarification before proceeding,
hence only the provided statuses will be left in for the time being.

### Decision 4: No security mechanism added

*Context:* It was not specified what the API usage is; human, service calles etc. 
As security is a non-functional concern, it can be added later without impacting the devised logic. Security is entirely omitted.
