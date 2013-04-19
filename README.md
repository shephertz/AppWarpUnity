AppWarpUnity
============

Unity3D library for AppWarp.You simply need to download [AppWarpUnity.dll](https://github.com/shephertz/AppWarpUnity/blob/master/v1.0/AppWarpUnity.dll?raw=true) and import it as an Asset in to your Unity project 
and you are all set!

We have also added Unity 4 sample to illustrate how you use AppWarp API inside your Unity3D project. Its a basic sample
that shows how to two users can connect and exchange their positions in real-time and see each others movements across a
scene. 

To use it follow these simple steps

1) Register at [AppHQ developer console](http://apphq.shephertz.com)

2) Create an app of typ 'AppWarp'. Note its API Key and Secret Key.

3) Go to the AppWarp tab on the left and add a room to this newly created app. Note the room id.

4) Download the sample. Use the above values and replace where indicated in appwarp.cs file. Build!


Please visit [AppWarp](http://appwarp.shephertz.com/) to learn more about the product.

[Getting Started](https://github.com/shephertz/AppWarp_WP7_SDK_DLL/wiki/Getting-Started)

[Reference](https://github.com/shephertz/AppWarp_WP7_SDK_DLL/wiki/Reference)

[FAQ](https://github.com/shephertz/AppWarp_JAVA_SDK_JAR/wiki/FAQ)

Note* If you are running the application as an Editor Application, you should listen to playmodeStateChanged events
and call the Disconnect API of WarpClient instance. This will prevent your editor from hanging when the app quits.
