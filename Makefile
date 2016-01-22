
all: demos

demos: InterfaceDemo.exe RunAndDisplayMap.exe

clean:
	find . -name \*.exe -delete

InterfaceDemo.exe:
	mcs interface/InterfaceDemo.cs interface/*.cs
	mv interface/Interface.exe .

RunAndDisplayMap.exe:
	mcs maps/RunAndDisplayMap.cs maps/*.cs
	mv maps/RunAndDisplayMap.exe .

.PHONY: all demos clean

