using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMBAY
{
    public class ResponseCode
    {
        public ResponseCode()
        {

        }

        public uint Approve
        {
            get
            {
                try
                {
                    return 00;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint IneligibleTransaction
        {
           get
            {
                try
                {
                    return 50;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint TransactionNotAuthorized
        {
            get
            {
                try
                {
                    return 51;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint UsesLimitExceeded
        {
            get
            {
                try
                {
                    return 52;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint AmountExceededLimit
        {
            get
            {
                try
                {
                    return 53;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InvalidAmount
        {
            get
            {
                try
                {
                    return 54;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InvalidRequest
        {
            get
            {
                try
                {
                    return 55;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint IneligibleAccount
        {
            get
            {
                try
                {
                    return 70;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint AccountIsClosed
        {
            get
            {
                try
                {
                    return 71;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint AccountNotAssign
        {
            //Account not assign, Contact bank
            get
            {
                try
                {
                    return 72;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint AccountNotAuthorize
        {
            //Account not authorize, Contact bank
            get
            {
                try
                {
                    return 73;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint AccountIsHeld
        {
            //Account is held, Contact bank
            get
            {
                try
                {
                    return 74;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InactiveAccount
        {
            //Inactive account, Contact bank
            get
            {
                try
                {
                    return 75;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint StatementInformationNotAvailable
        {
            get
            {
                try
                {
                    return 77;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InsufficientFund
        {
            get
            {
                try
                {
                    return 78;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InvalidAccountType
        {
            //Invalid Account Type or Contact number
            get
            {
                try
                {
                    return 79;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint SystemError
        {
            get
            {
                try
                {
                    return 80;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint RoutingLookupProblem
        {
            //Routing lookup problem, Unable to process
            get
            {
                try
                {
                    return 81;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint DatabaseProblem
        {
            get
            {
                try
                {
                    return 82;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint InvalidMessageAuthenticationCode
        {
            get
            {
                try
                {
                    return 83;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint MessageEditError
        {
            //Message edit error (Invalid Message)
            get
            {
                try
                {
                    return 84;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint DestinationNotAvailable
        {
            get
            {
                try
                {
                    return 87;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint ExternalDecline
        {
            get
            {
                try
                {
                    return 88;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public uint SystemNotAvailable
        {
            get
            {
                try
                {
                    return 89;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

    }
}