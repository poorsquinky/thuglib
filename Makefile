
all: demos

demos: Interface.exe RunAndDisplayMap.exe

clean:
	find . -name \*.exe -delete

Interface.exe:
	mcs interface/*.cs
	mv interface/Interface.exe .

RunAndDisplayMap.exe:
	mcs maps/RunAndDisplayMap.cs maps/*.cs
	mv maps/RunAndDisplayMap.exe .

.PHONY: all demos clean

