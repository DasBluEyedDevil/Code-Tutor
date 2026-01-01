def p(l):
    r=[]
    for i in l:
        if i%2==0:
            r.append(i*2)
    return r

class C:
    def __init__(self,n,a):
        self.n=n
        self.a=a
    def g(self):
        return self.n if self.a else None