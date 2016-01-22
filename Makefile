
all: demos

demos: InterfaceDemo.exe RunAndDisplayMap.exe

clean:
	find . -name \*.exe -delete

InterfaceDemo.exe: $(wildcard interface/I.cs)
	mcs interface/InterfaceDemo.cs interface/*.cs
	mv interface/InterfaceDemo.exe .

RunAndDisplayMap.exe: $(wildcard maps/*.cs)
	mcs maps/RunAndDisplayMap.cs maps/*.cs
	mv maps/RunAndDisplayMap.exe .

.PHONY: all demos clean

