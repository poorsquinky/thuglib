
all: demos

demos: InterfaceDemo.exe RunAndDisplayMap.exe InterfaceMapDemo.exe

clean:
	find . -name \*.exe -delete

InterfaceDemo.exe: demo/InterfaceDemo.cs $(wildcard interface/*.cs)
	mcs $^
	mv demo/InterfaceDemo.exe .

RunAndDisplayMap.exe: demo/RunAndDisplayMap.cs $(wildcard maps/*.cs)
	mcs $^
	mv demo/RunAndDisplayMap.exe .

InterfaceMapDemo.exe: InterfaceMapDemo.cs $(wildcard interface/*.cs) maps/*.cs
	mcs $^

.PHONY: all demos clean

