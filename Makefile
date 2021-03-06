
all: demos

#demos: UIDemo.exe RunAndDisplayMap.exe UIMapDemo.exe
demos: RunAndDisplayMap.exe UIMapDemo.exe

clean:
	find . -name \*.exe -delete

UIDemo.exe: demo/UIDemo.cs $(wildcard ui/*.cs) $(wildcard utils/*.cs)
	mcs -debug $^
	mv demo/UIDemo.exe .

RunAndDisplayMap.exe: demo/RunAndDisplayMap.cs $(wildcard maps/*.cs) $(wildcard utils/*.cs)
	mcs -debug $^
	mv demo/RunAndDisplayMap.exe .

UIMapDemo.exe: demo/UIMapDemo.cs $(wildcard ui/*.cs) $(wildcard maps/*.cs) $(wildcard utils/*.cs)
	mcs -debug $^
	mv demo/UIMapDemo.exe .

.PHONY: all demos clean

