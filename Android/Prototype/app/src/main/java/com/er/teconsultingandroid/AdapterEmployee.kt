package com.er.teconsultingandroid

import android.content.Context
import android.content.Intent
import android.util.Log
import android.view.ContextMenu
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageButton
import android.widget.TextView
import androidx.appcompat.view.menu.MenuView
import androidx.recyclerview.widget.RecyclerView

class AdapterEmployee(private val context:Context, private val employeeArrayList: ArrayList<ModelEmployee>) : RecyclerView.Adapter<AdapterEmployee.HolderPost>(){

    private val TAG = "MAIN_TAG"

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): HolderPost {
        //inflate layout row_post.xml
        val view = LayoutInflater.from(context).inflate(R.layout.row_post,parent, false)
        return HolderPost(view)
    }

    override fun onBindViewHolder(holder: HolderPost, position: Int) {
        //get data, set data, format data, handle click, etc
        val model = employeeArrayList[position] //get data at specified index

        // get data
        val employeeId = model.id
        val firstName = model.firstName
        val middleInitial = model.middleName
        val lastName = model.lastName
        val workNum = model.workNum
        val departmentName = model.departmentName
        val jobName = model.jobName
        var fullName = "$firstName $lastName"
        val province = model.province
        val country = model.country

        var position = "Department: $departmentName\nPosition: $jobName"

        if(middleInitial != "")
        fullName = "$firstName $middleInitial $lastName"

        val outputEmployee = "$fullName\n$workNum\n$position\nLocation: $province, $country"

        holder.titleTv.text = outputEmployee

        holder.itemView.setOnClickListener {
            val intent = Intent(context, EmployeeDetailsActivity::class.java)
            Log.d(TAG, "THIS IS EMP ID: ${employeeId}")
            intent.putExtra("employeeId", employeeId.toString()) //key value pair to get employee details in employeedetailsactivity
            context.startActivity(intent)
        }
    }

    override fun getItemCount(): Int {
        //returns number of items/records/list_size
      return employeeArrayList.size
    }


    //ViewHolder class, holds initial UI views of row_post

    inner class HolderPost(itemView: View) : RecyclerView.ViewHolder(itemView){

        //initialize UI Views
        var rButton:ImageButton = itemView.findViewById(R.id.rButton)
        var titleTv:TextView = itemView.findViewById(R.id.titleTv)
    }
}