echo Brainfudge Wrapper v0.1
echo 'Do you want to interpret (i) Brainfuck code or convert (c) text to Brainfuck' 
read choice
if [ "$choice" = "i" ]; then
	echo "Input the text to interpret"
	read toInt
	IntOut=$(./Brainpreter -i $toInt )
	echo "$IntOut"
elif [ "$choice" = "c" ]; then
	echo "Input the text to convert"
        read toCon
        ConOut=$(./Brainpreter -c $toCon ) 
        echo "$ConOut" 
else
	echo "Thats not a valid choice"
fi
