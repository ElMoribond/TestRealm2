<h2>Attempt to use Realm with a hybrid service.</h2>

Use REalm in a Android background service.

This simple page works and only displays a counter every 5 seconds in the logcat window.<br>

Before executing it, check/adapt the first lines of the Constants.cs file.
To create the user 2 possibilities
- manually, the model is in Helpers.cs
- automatically by modifying createUser to true in the Counter.cs file and then run once before setting false.

I'm having trouble when I want to use the same realm from the UI and service.

What surprises me most is that it worked 3 times during my tests (see Success.png).
