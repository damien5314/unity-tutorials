#import "TestPlugin.h"

@implementation TestObject
@end

NSString* CreateNSString (const char* string) {
if (string)
return [NSString stringWithUTF8String: string];
else
return [NSString stringWithUTF8String: ""];
}

char* MakeStringCopy (const char* string) {
if (string == NULL)
return NULL;
char* res = (char*)malloc(strlen(string) + 1);
strcpy(res, string);
return res;
}

extern "C" {
	const char* _TestString(const char* string) {
		NSString* oldString = CreateNSString(string);
		NSString* newString = [oldString uppercaseString];
		return MakeStringCopy([newString UTF8String]);
	}
	
	float _TestNumber() {
		return (arc4random() % 100) / 100.0f;
	}
}
