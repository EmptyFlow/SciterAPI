﻿using System.Runtime.InteropServices;
using System.Text;

namespace SciterLibraryAPI.SystemApis {

    public enum CFStringEncoding : uint {
        UTF16 = 0x0100,
        UTF16BE = 0x10000100,
        UTF16LE = 0x14000100,
        ASCII = 0x0600
    }

    internal static class MacOsApis {

        private const string FoundationFramework = "/System/Library/Frameworks/Foundation.framework/Foundation";

        private const string AppKitFramework = "/System/Library/Frameworks/AppKit.framework/AppKit";

        [DllImport ( FoundationFramework )]
        public static extern IntPtr CFStringCreateWithBytes ( IntPtr allocator, IntPtr buffer, long bufferLength, CFStringEncoding encoding, bool isExternalRepresentation );

        [DllImport ( FoundationFramework )]
        public static extern IntPtr CFArrayCreate ( IntPtr allocator, IntPtr values, long numValues, IntPtr callbackStruct );

        [DllImport ( FoundationFramework )]
        public static extern void CFRetain ( IntPtr handle );

        [DllImport ( FoundationFramework )]
        public static extern void CFRelease ( IntPtr handle );

        [DllImport ( AppKitFramework, CharSet = CharSet.Ansi )]
        public static extern IntPtr objc_getClass ( string name );

        [DllImport ( AppKitFramework )]
        public static extern IntPtr NSSelectorFromString ( IntPtr cfstr );

        [DllImport ( FoundationFramework, EntryPoint = "objc_msgSend" )]
        public static extern IntPtr objc_msgSend_retIntPtr ( IntPtr target, IntPtr selector );

        [DllImport ( FoundationFramework, EntryPoint = "objc_msgSend" )]
        public static extern void objc_msgSend_retVoid_IntPtr_IntPtr ( IntPtr target, IntPtr selector, IntPtr param1, IntPtr param2 );

        [DllImport ( FoundationFramework, EntryPoint = "objc_msgSend" )]
        public static extern IntPtr objc_msgSend_retIntPtr_IntPtr ( IntPtr target, IntPtr selector, IntPtr param );

        [DllImport ( FoundationFramework, EntryPoint = "objc_msgSend" )]
        public static extern void objc_msgSend_retVoid ( IntPtr target, IntPtr selector );

        public unsafe static IntPtr CreateCFString ( string aString ) {
            var bytes = Encoding.Unicode.GetBytes ( aString );
            fixed ( byte* b = bytes ) {
                var cfStr = CFStringCreateWithBytes ( IntPtr.Zero, (IntPtr) b, bytes.Length, CFStringEncoding.UTF16, false );
                return cfStr;
            }
        }

        public static IntPtr GetSelector ( string name ) {
            IntPtr cfstrSelector = CreateCFString ( name );
            IntPtr selector = NSSelectorFromString ( cfstrSelector );
            CFRelease ( cfstrSelector );
            return selector;
        }

        public static void CreateNsApplicationAndRun() {
            var application = objc_getClass ( "NSApplication" );
            var sharedWorkspace = objc_msgSend_retIntPtr ( application, GetSelector ( "sharedApplication" ) );

            objc_msgSend_retVoid ( sharedWorkspace, GetSelector ( "run" ) );

            CFRelease ( sharedWorkspace );

            //NSApplication * application = [NSApplication sharedApplication];
            //[application run];
        }

    }

    /*
     namespace NSWorkspaceExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var testFile = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            File.WriteAllText(testFile, "example file content");

            var cfstrTestFile = CreateCFString(testFile);
            var nsURL = objc_getClass("NSURL");
            var fileUrl = objc_msgSend_retIntPtr_IntPtr(nsURL, GetSelector("fileURLWithPath:"), cfstrTestFile);
            CFRelease(cfstrTestFile);

            var urlArray = CreateCFArray(new IntPtr[] {fileUrl});

            var nsWorkspace = objc_getClass("NSWorkspace");
            var sharedWorkspace = objc_msgSend_retIntPtr(nsWorkspace, GetSelector("sharedWorkspace"));

            objc_msgSend_retVoid_IntPtr_IntPtr(sharedWorkspace, GetSelector("recycleURLs:completionHandler:"), urlArray, IntPtr.Zero);

            CFRelease(urlArray);
            CFRelease(fileUrl);
            CFRelease(sharedWorkspace);

            // sleep since we didn't go through the troubles of creating a block object as a callback
            Thread.Sleep(1000);
        }

        public static IntPtr GetSelector(string name)
        {
            IntPtr cfstrSelector = CreateCFString(name);
            IntPtr selector = NSSelectorFromString(cfstrSelector);
            CFRelease(cfstrSelector);
            return selector;
        }


        public unsafe static IntPtr CreateCFString(string aString)
        {
            var bytes = Encoding.Unicode.GetBytes(aString);
            fixed (byte* b = bytes) {
                var cfStr = CFStringCreateWithBytes(IntPtr.Zero, (IntPtr)b, bytes.Length, CFStringEncoding.UTF16, false);
                return cfStr;
            }
        }

        // warning: this doesn't call retain/release on the elements in the array
        public unsafe static IntPtr CreateCFArray(IntPtr[] objectes)
        {
            fixed(IntPtr* vals = objectes) {
                 return CFArrayCreate(IntPtr.Zero, (IntPtr)vals, objectes.Length, IntPtr.Zero);
            }
        }
    }
}
     */
}
