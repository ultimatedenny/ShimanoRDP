*********************
Command-Line Switches
*********************

The following commandlline switches are supported by ShimanoRDP:

``/cons: PathToConnectionsFile``
``/c: PathToConnectionsFile``

 Loads the connections file from the given path. This path can be a:
 - full file path
 - path relative to the current directory
 - path relative to the ShimanoRDP application directory
 - path relative to the ShimanoRDP default connection file directory

``/reset``

 Resets window position, panels and toolbars

``/resetpos``
``/rp``

 Reset the windows position

``/resetpanels``
``/rpnl``

 Resets all panel positions. Use this if you have troubles with panel layouts

``/resettoolbar``
``/rtbr``

 Resets the positions of all toolbars

``/noreconnect``
``/norc``

 Temporary disables reconnect to previously opened sessions.
 Use this if you have problems opening ShimanoRDP after you
 enabled the setting and restarted ShimanoRDP
