namespace SciterLibraryAPI {
    /*
     public struct SCRIPTING_METHOD_PARAMS_WRAPPER
		{
			public SCRIPTING_METHOD_PARAMS_WRAPPER(SCRIPTING_METHOD_PARAMS prms)
			{
				name = Marshal.PtrToStringAnsi(prms.name);
				args = new SciterCore.SciterValue[prms.argc];
				result = SciterCore.SciterValue.Undefined;

				for (var i = 0; i < prms.argc; i++)
				{
					var ptr = IntPtr.Add(prms.argv,
						i * Marshal.SizeOf(typeof(SciterValue.VALUE)));

					args[i] = SciterCore.SciterValue.Attach(Marshal.PtrToStructure<SciterValue.VALUE>(ptr));
				}
			}

			public string name;
			public SciterCore.SciterValue[] args;
			public SciterCore.SciterValue result;
		}
     */

    public enum InitializationEvents : uint { // Original name INITIALIZATION_EVENTS
        BEHAVIOR_DETACH = 0,
        BEHAVIOR_ATTACH = 1
    }

}
