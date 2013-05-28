AppWarpUnity
============

[![OverView](http://appwarp.shephertz.com/images/appwarp_logo.png)](http://appwarp.shephertz.com)

Unity3D library for AppWarp.You simply need to download [AppWarpUnity.dll](https://github.com/shephertz/AppWarpUnity/blob/master/v1.1/AppWarpUnity.dll?raw=true) and import it as an Asset in to your Unity project 
and you are all set!

We have also added a Unity 4 sample to illustrate how you use AppWarp API inside your Unity3D project. Its a basic sample
that shows how to two users can connect and exchange their positions in real-time and see each others movements across a
scene. 

*  <a href="http://blogs.shephertz.com/2013/04/30/make-real-time-multiplayer-games-using-unity3d/" target="_blank">Introduction Blog Post</a>
*  [Registering and getting your Application keys](Using-AppHQ)
*  [Basic Concepts](Basic-Concepts)
*  [Using AppWarp C# SDK](C%23-Getting-Started)
*  [AppWarp C# API Reference](C%23-API-Reference)
*  [Download Unity Asset](https://github.com/shephertz/AppWarpUnity/)
*  [Complete Sample Walkthrough](Unity-Sample-WalkThrough)


Note* If you are running the application as an Editor Application, you should listen to playmodeStateChanged events
and call the Disconnect API of WarpClient instance. This will prevent your editor from hanging when the app quits.
