
all: demos

demos: Display.exe RunAndDisplayMap.exe

clean:
	find . -name \*.exe -delete

Display.exe:
	mcs display/*.cs
	mv display/Display.exe .

RunAndDisplayMap.exe:
	mcs maps/RunAndDisplayMap.cs maps/*.cs
	mv maps/RunAndDisplayMap.exe .

.PHONY: all demos clean

