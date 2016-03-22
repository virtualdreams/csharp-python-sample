# core.py
from core import *

# time
import time

# import from assembly
from python_sample import User

# function
def mul(x):
	return x * x

# variable
var = "c"

m = ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni', 'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember']

t = '12.10.2015'
t = time.strptime(t, '%d.%m.%Y')

var = '{h}. {m} {y}'.format(h = t.tm_mday, m = m[t.tm_mon - 1], y = t.tm_year)

# class
class Example(Base):
	
	def __init__(self):
		pass
		
	def Multiply(self, e):
		return e * e

	def Create(self):
		v = User()
		v.Id = 500
		v.Name = "Script"
		v.IId = 1

		return v
		
	def Usage(self):
		return 'Sample'
		
		
		