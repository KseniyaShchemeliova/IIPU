sudo hdparm -i /dev/sda |grep Model
sudo hdparm -i /dev/sda |grep ATA
sudo hdparm -i /dev/sda |grep PIO |grep -v tPIO
sudo hdparm -i /dev/sda |grep DMA |grep -v tDMA |grep -v UDMA
df | awk '{ size+=$2; used+=$3; avial+=$4} END {print "Size" size "\n" "Used" used "\n" "Avial" avial}'