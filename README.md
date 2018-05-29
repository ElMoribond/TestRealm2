<h2>Attempt to use Realm with a hybrid service.</h2>

Use REalm in a Android background service.

This simple page works and only displays a counter every 5 seconds in the logcat window.<br>
This counter is displayed by a hybrid service.

Before executing it, check/adapt the first lines of the Constants.cs file.
To create the user 2 possibilities
- manually, the model is in Helpers.cs
- automatically by modifying createUser to true in the Counter.cs file and then run once before setting false.

The final goal is for the service to write to the realm and be displayed by the UI.

I'm having trouble when I want to use the same realm from the UI and service.
