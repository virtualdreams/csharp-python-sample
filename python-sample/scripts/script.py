# core.py
from core import *

# import from assembly
from python_sample import User

# function
def mul(x):
	return x * x

# variable
var = "c"

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
		
		
		