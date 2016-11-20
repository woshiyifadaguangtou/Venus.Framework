using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venus.Application.Entity
{
    public class MsgRecordEntity
	{
	
      	/// <summary>
		/// ID
        /// </summary>
        public virtual int ID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UserName
        /// </summary>
        public virtual string UserName
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UserNO
        /// </summary>
        public virtual string UserNO
        {
            get; 
            set; 
        }        
		/// <summary>
		/// UserAddress
        /// </summary>
        public virtual string UserAddress
        {
            get; 
            set; 
        }        
		/// <summary>
		/// PhoneNO
        /// </summary>
        public virtual string PhoneNO
        {
            get; 
            set; 
        }        
		/// <summary>
		/// DueMoney
        /// </summary>
        public virtual decimal DueMoney
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MsgState
        /// </summary>
        public virtual string MsgState
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateDate
        /// </summary>
        public virtual DateTime CreateDate
        {
            get; 
            set; 
        }        
		/// <summary>
		/// SendIndexID
        /// </summary>
        public virtual string SendIndexID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// SendTime
        /// </summary>
        public virtual DateTime? SendTime
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MessageType
        /// </summary>
        public virtual string MessageType
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ReceiveState
        /// </summary>
        public virtual string ReceiveState
        {
            get; 
            set; 
        }        
		/// <summary>
		/// FeeCounts
        /// </summary>
        public virtual int? FeeCounts
        {
            get; 
            set; 
        }        
		/// <summary>
		/// TaskID
        /// </summary>
        public virtual int TaskID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MsgContent
        /// </summary>
        public virtual string MsgContent
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RemainMoney
        /// </summary>
        public virtual decimal? RemainMoney
        {
            get; 
            set; 
        }        
		/// <summary>
		/// pwf
        /// </summary>
        public virtual decimal? pwf
        {
            get; 
            set; 
        }        
		/// <summary>
		/// ljf
        /// </summary>
        public virtual decimal? ljf
        {
            get; 
            set; 
        }        
		/// <summary>
		/// bysf
        /// </summary>
        public virtual decimal? bysf
        {
            get; 
            set; 
        }        
    }
}
