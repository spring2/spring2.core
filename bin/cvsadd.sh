# $1 = directory to change to
# $2 = name of file to add
# $3 = commit message
cd $1
cvs add -kb $1
cvs commit -m $3 $1