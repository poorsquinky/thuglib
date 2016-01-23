
all: demos

demos: UIDemo.exe RunAndDisplayMap.exe UIMapDemo.exe

clean:
	find . -name \*.exe -delete

UIDemo.exe: demo/UIDemo.cs $(wildcard ui/*.cs)
	mcs $^
	mv demo/UIDemo.exe .

RunAndDisplayMap.exe: demo/RunAndDisplayMap.cs $(wildcard maps/*.cs)
	mcs $^
	mv demo/RunAndDisplayMap.exe .

UIMapDemo.exe: demo/UIMapDemo.cs $(wildcard ui/*.cs) $(wildcard maps/*.cs)
	mcs $^
	mv demo/UIMapDemo.exe .

.PHONY: all demos clean

