class Payment:
    def __init__(self, amount):
        self.amount = amount
    
    def pay(self):
        # TODO: Raise NotImplementedError
        pass

class CreditCard(Payment):
    def __init__(self, amount, card_number):
        # TODO: Call parent init and store card_number
        pass
    
    def pay(self):
        # TODO: Return payment message
        pass

class PayPal(Payment):
    def __init__(self, amount, email):
        # TODO: Call parent init and store email
        pass
    
    def pay(self):
        # TODO: Return payment message
        pass

class Bitcoin(Payment):
    def __init__(self, amount, wallet_address):
        # TODO: Call parent init and store wallet_address
        pass
    
    def pay(self):
        # TODO: Return payment message
        pass

def process_payment(payment):
    # TODO: Process any payment type
    pass

# TODO: Create different payment types and process them